import { Camera, Clock } from "lucide-react";

export function GalleryCard() {
  return (
    <div className="bg-white rounded-xl shadow border border-gray-200 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer group">
      <div className="p-8 text-center">
        <div className="flex justify-center mb-4">
          <div className="p-4 bg-gray-100 rounded-full group-hover:bg-gray-200 transition-colors">
            <Camera className="h-10 w-10 text-gray-600 group-hover:text-gray-700 transition-colors" />
          </div>
        </div>

        <h3 className="text-lg font-semibold text-gray-900 mb-2">
          Galeria de Evolução
        </h3>

        <div className="flex items-center justify-center gap-2 text-gray-500 mb-4">
          <Clock className="h-4 w-4" />
          <span className="text-mg italic">Em construção</span>
        </div>

        <p className="text-mg text-gray-600 leading-relaxed">
          Acompanhe sua evolução através de fotos comparativas
        </p>
      </div>
    </div>
  );
}
