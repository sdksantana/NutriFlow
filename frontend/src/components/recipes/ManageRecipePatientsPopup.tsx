import { useEffect, useState } from "react";
import { RecipeWithLinks } from "../../types/RecipeTypes";
import { Users, X, Plus } from "lucide-react";
import api from "../../api/axios";
import { toast } from "react-toastify";
import { Endpoints } from "../../routes/endpoints";

interface Patient {
  id: string;
  name: string;
  recipeLinkId?: string;
}

interface Props {
  recipe: RecipeWithLinks;
  onClose: (changed: boolean) => void;
  onUpdate: () => void;
}

export function ManageRecipePatientsPopup({
  recipe,
  onClose,
  onUpdate,
}: Props) {
  const [linkedPatients, setLinkedPatients] = useState<Patient[]>([]);
  const [allPatients, setAllPatients] = useState<Patient[]>([]);
  const [selectedPatientId, setSelectedPatientId] = useState("");
  const [hasChanged, setHasChanged] = useState(false);
  const [updatedRecipe, setUpdatedRecipe] = useState<RecipeWithLinks>(recipe);
  const nutritionistId = localStorage.getItem("userId");

  async function fetchRecipeAndLinks() {
    if (!nutritionistId || !recipe.id) return;

    try {
      const res = await api.get(
        Endpoints.getNutritionistRecipeById(recipe.id, nutritionistId)
      );
      const fetchedRecipe: RecipeWithLinks = res.data;
      setUpdatedRecipe(fetchedRecipe);
      setLinkedPatients(
        (fetchedRecipe.links || []).map((item) => ({
          id: item.patientId,
          name: item.patientName,
          recipeLinkId: item.recipeLinkId,
        }))
      );
    } catch {
      toast.error("Erro ao atualizar vínculos da receita.");
    }
  }

  async function fetchPatients() {
    if (!nutritionistId) return;

    try {
      const profileRes = await api.get(Endpoints.getProfile(nutritionistId));
      const profilePatients = profileRes.data.linkedPatients || [];
      setAllPatients(
        profilePatients.map((p: any) => ({
          id: p.id,
          name: `${p.firstName} ${p.lastName}`,
        }))
      );
    } catch {
      toast.error("Erro ao carregar pacientes.");
    }
  }
  useEffect(() => {
    fetchPatients();
    fetchRecipeAndLinks();
  }, [recipe.id]);

  const availablePatients = allPatients.filter(
    (p) => !linkedPatients.some((lp) => lp.id === p.id)
  );

  async function handleAddPatient() {
    if (!selectedPatientId || !nutritionistId || !updatedRecipe.id) return;

    try {
      await api.post(Endpoints.addRecipeLink(nutritionistId), {
        nutritionistRecipeId: updatedRecipe.id,
        patientId: selectedPatientId,
      });

      setSelectedPatientId("");
      await fetchRecipeAndLinks();
      toast.success("Paciente associado com sucesso!");
      setHasChanged(true);
      onUpdate();
    } catch {
      toast.error("Erro ao associar paciente.");
    }
  }
  async function handleRemovePatient(patient: Patient) {
    if (!nutritionistId || !patient.recipeLinkId) {
      toast.error("Informações incompletas para remover associação.");
      return;
    }
    try {
      await api.delete(
        Endpoints.deleteRecipeLink(patient.recipeLinkId, nutritionistId)
      );
      toast.success("Associação removida.");
      await fetchRecipeAndLinks();
      setHasChanged(true);
      onUpdate();
    } catch {
      toast.error("Erro ao remover associação.");
    }
  }
  return (
    <div className="fixed inset-0 z-50 bg-black/40 flex items-center justify-center px-4">
      <div className="bg-white w-full max-w-md rounded-2xl shadow-lg border border-gray-200 p-6 relative">
        <button
          onClick={() => onClose(hasChanged)}
          className="absolute top-4 right-4 text-gray-400 hover:text-gray-600"
        >
          <X className="w-5 h-5" />
        </button>

        <div className="mb-6">
          <h2 className="text-lg font-semibold flex items-center gap-2 text-gray-800 mb-1">
            <Users className="w-5 h-5" />
            Pacientes Associados
          </h2>
          <p className="text-sm text-gray-600">
            Receita: <span className="font-medium">{updatedRecipe.name}</span>
          </p>
        </div>

        <div className="mb-6">
          <h4 className="text-sm font-medium text-gray-700 mb-3">
            Pacientes que recebem esta receita:
          </h4>
          {linkedPatients.length === 0 ? (
            <div className="text-center py-4 text-gray-500 bg-gray-50 rounded-lg">
              <Users className="w-8 h-8 mx-auto mb-2 text-gray-300" />
              <p className="text-sm">Nenhum paciente associado ainda</p>
            </div>
          ) : (
            <div className="space-y-2">
              {linkedPatients.map((patient) => (
                <div
                  key={patient.id}
                  className="flex items-center justify-between p-3 bg-green-50 border border-green-200 rounded-lg"
                >
                  <div className="flex items-center gap-2">
                    <div className="w-2 h-2 bg-green-500 rounded-full" />
                    <span className="text-sm font-medium text-gray-900">
                      {patient.name}
                    </span>
                  </div>
                  <button
                    onClick={() => handleRemovePatient(patient)}
                    className="text-red-600 hover:text-red-700 hover:bg-red-50 rounded p-1"
                  >
                    <X className="w-4 h-4" />
                  </button>
                </div>
              ))}
            </div>
          )}
        </div>

        <div className="mb-6">
          <h4 className="text-sm font-medium text-gray-700 mb-3">
            Adicionar paciente:
          </h4>
          {availablePatients.length === 0 ? (
            <div className="text-center py-3 text-gray-500 bg-gray-50 rounded-lg">
              <p className="text-sm">Todos os pacientes já estão associados</p>
            </div>
          ) : (
            <div className="flex gap-2">
              <select
                className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm text-gray-800"
                value={selectedPatientId}
                onChange={(e) => setSelectedPatientId(e.target.value)}
              >
                <option value="">Selecione um paciente...</option>
                {availablePatients.map((p) => (
                  <option key={p.id} value={p.id}>
                    {p.name}
                  </option>
                ))}
              </select>
              <button
                onClick={handleAddPatient}
                disabled={!selectedPatientId}
                className="bg-green-600 hover:bg-green-700 text-white px-3 py-2 rounded-lg disabled:opacity-50"
              >
                <Plus className="w-4 h-4" />
              </button>
            </div>
          )}
        </div>

        <div className="flex justify-between items-center pt-4 border-t">
          <span className="text-sm text-gray-600">
            {linkedPatients.length} paciente(s) associado(s)
          </span>
          <button
            onClick={() => onClose(hasChanged)}
            className="bg-orange-500 hover:bg-orange-600 text-white px-5 py-2 rounded-lg"
          >
            Concluir
          </button>
        </div>
      </div>
    </div>
  );
}
