import Layout from "../../../components/layouts/AppLayout";
import { IMCIndicator } from "../../../components/evolution/IMCIndicator";
import { TMBCard } from "../../../components/evolution/TMBCard";
import { GalleryCard } from "../../../components/evolution/GalleryCard";
import { MeasurementHistory } from "../../../components/evolution/MeasurementHistory";
import { EvolutionChartCard } from "../../../components/evolution/EvolutionChartCard";

export default function Dashboard() {
  return (
    <Layout>
      <div className="min-h-screen bg-gradient-to-br from-orange-50 to-amber-50 py-8 px-4">
        <div className="max-w-7xl mx-auto space-y-10">
          <div className="text-center">
            <h1 className="text-4xl font-bold text-gray-900 mb-2">
              Dashboard de Saúde
            </h1>
            <p className="text-gray-600 text-lg">
              Acompanhe sua evolução e mantenha-se motivado
            </p>
          </div>
          <div className="max-w-4xl mx-auto">
            <IMCIndicator />
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6 max-w-4xl mx-auto">
            <TMBCard />
            <GalleryCard />
          </div>
          <EvolutionChartCard />
          <MeasurementHistory />
        </div>
      </div>
    </Layout>
  );
}
