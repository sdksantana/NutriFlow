interface IMCBarProps {
  imc: number;
}

export function IMCBar({ imc }: IMCBarProps) {
  const ranges = [
    {
      label: "ABAIXO DO PESO",
      min: 0,
      max: 18.4,
      color: "bg-cyan-600",
      textColor: "text-cyan-700",
    },
    {
      label: "SAUDÁVEL",
      min: 18.5,
      max: 24.9,
      color: "bg-emerald-600",
      textColor: "text-emerald-700",
    },
    {
      label: "SOBREPESO",
      min: 25,
      max: 29.9,
      color: "bg-yellow-600",
      textColor: "text-yellow-700",
    },
    {
      label: "OBESIDADE I",
      min: 30,
      max: 34.9,
      color: "bg-orange-600",
      textColor: "text-orange-700",
    },
    {
      label: "OBESIDADE II",
      min: 35,
      max: 39.9,
      color: "bg-violet-600",
      textColor: "text-violet-700",
    },
    {
      label: "OBESIDADE III",
      min: 40,
      max: 100,
      color: "bg-red-800",
      textColor: "text-red-900",
    },
  ];
  const currentRangeIndex = ranges.findIndex(
    (r) => imc >= r.min && imc <= r.max
  );
  const currentRange = ranges[currentRangeIndex];
  const step = 100 / ranges.length;
  const trianglePosition = step * currentRangeIndex + step / 2;

  return (
    <div className="w-full space-y-4 rounded-xl shadow border border-gray-200 p-4 bg-white">
      <div className="grid md:hidden grid-cols-2 gap-2 text-xs text-gray-700 font-medium text-center">
        <div>IMC: {imc.toFixed(1)}</div>
        <div className={`${currentRange?.textColor}`}>
          {currentRange?.label}
        </div>
      </div>

      <div className="hidden md:grid grid-cols-6 gap-1 text-mg font-bold text-gray-600 text-center">
        {ranges.map((range) => (
          <div key={range.label} className="leading-tight px-2">
            {range.label}
          </div>
        ))}
      </div>

      <div className="relative h-6 flex items-center">
        <div
          className="absolute transition-all duration-500 ease-out"
          style={{ left: `calc(${trianglePosition}% - 10px)` }}
        >
          <div className="w-0 h-0 border-l-[10px] border-r-[10px] border-b-[14px] border-l-transparent border-r-transparent border-b-gray-800 opacity-80" />
        </div>
      </div>

      <div className="flex h-4 rounded-full overflow-hidden shadow-inner bg-gray-100">
        {ranges.map((range, index) => (
          <div
            key={range.label}
            className={`flex-1 ${range.color} transition-all duration-300 ${
              index === currentRangeIndex ? "ring-1 ring-black ring-inset" : ""
            }`}
          />
        ))}
      </div>
      <div className="text-center space-y-2 hidden md:block">
        <div className="text-2xl font-bold text-gray-900">
          IMC: {imc.toFixed(1)}
        </div>
        {currentRange && (
          <div className={`text-mg font-bold ${currentRange.textColor}`}>
            {currentRange.label}
          </div>
        )}
      </div>
    </div>
  );
}
