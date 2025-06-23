export const macroColors: Record<string, Record<string, string>> = {
  carbs: {
    "0-10": "bg-green-200 text-green-900",
    "11-20": "bg-blue-200 text-blue-900",
    "21-30": "bg-yellow-200 text-yellow-900",
    "31-40": "bg-orange-200 text-orange-900",
    "41-50": "bg-amber-200 text-amber-900",
    "50+": "bg-red-200 text-red-900",
  },
  protein: {
    "0-10": "bg-green-200 text-green-900",
    "11-20": "bg-blue-200 text-blue-900",
    "21-30": "bg-yellow-200 text-yellow-900",
    "30+": "bg-red-200 text-red-900",
  },
  fat: {
    "0-5": "bg-green-200 text-green-900",
    "6-10": "bg-blue-200 text-blue-900",
    "11-15": "bg-yellow-200 text-yellow-900",
    "16-20": "bg-orange-200 text-orange-900",
    "20+": "bg-red-200 text-red-900",
  },
  calories: {
    "100-199": "bg-green-200 text-green-900",
    "200-299": "bg-blue-200 text-blue-900",
    "300-399": "bg-yellow-200 text-yellow-900",
    "400-499": "bg-orange-200 text-orange-900",
    "500+": "bg-red-200 text-red-900",
  },
};

export function getBadgeColor(value: string, macro: string): string {
  return macroColors[macro]?.[value] || macroColors[macro][Object.keys(macroColors[macro])[0]];
}