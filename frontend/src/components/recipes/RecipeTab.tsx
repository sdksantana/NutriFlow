interface Props {
  activeTab: "receitas" | "plano";
  setActiveTab: (v: "receitas" | "plano") => void;
  mode?: "patient" | "nutritionist";
}

export function RecipeTab({
  activeTab,
  setActiveTab,
  mode = "patient",
}: Props) {
  return (
    <div className="flex justify-start gap-4 mb-3">
      <button
        onClick={() => setActiveTab("receitas")}
        className={`
          px-6 py-2 rounded-full transition-colors
          ${
            activeTab === "receitas"
              ? "bg-orange-200 font-semibold text-orange-500"
              : "bg-transparent text-gray-700 hover:bg-orange-100"
          }
        `}
      >
        Receitas
      </button>

      {mode === "patient" && (
        <button
          onClick={() => setActiveTab("plano")}
          className={`
            px-6 py-2 rounded-full transition-colors
            ${
              activeTab === "plano"
                ? "bg-orange-200 font-semibold text-orange-500"
                : "bg-transparent text-gray-700 hover:bg-orange-100"
            }
          `}
        >
          Plano Alimentar
        </button>
      )}
    </div>
  );
}
