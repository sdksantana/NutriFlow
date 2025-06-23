import { RefObject } from "react";
import { FilterOptions } from "../../types/RecipeTypes";
import { ChevronDown } from "lucide-react";
import { placeholders } from "../../utils/recipe";

interface Props {
  filters: { key: keyof FilterOptions; label: string }[];
  filterOptions: FilterOptions;
  selectedFilters: Record<string, string | null>;
  openFilter: string | null;
  setOpenFilter: (key: string | null) => void;
  onChange: (key: string, value: string) => void;
  filterRef: RefObject<HTMLDivElement>;
}

export function FiltersBar({
  filters,
  filterOptions,
  selectedFilters,
  openFilter,
  setOpenFilter,
  onChange,
  filterRef,
}: Props) {
  return (
    <div className="flex flex-wrap gap-3 mb-8" ref={filterRef}>
      {filters.map(({ key, label }) => {
        const options = filterOptions[key] ?? [];
        const selectedValue = selectedFilters[key];
        const selectedLabel =
          options.find((opt) => opt.value === selectedValue)?.label ||
          placeholders[key] ||
          label;

        return (
          <div key={key.toString()} className="relative">
            <button
              type="button"
              aria-haspopup="listbox"
              aria-expanded={openFilter === key}
              className={`flex items-center gap-2 px-4 py-2 rounded-full border text-sm font-medium transition ${
                selectedValue
                  ? "bg-orange-100 border-orange-300 text-orange-600"
                  : "bg-white border-gray-300 text-gray-600 hover:bg-gray-50"
              }`}
              onClick={() => setOpenFilter(openFilter === key ? null : key)}
            >
              {selectedLabel}
              <ChevronDown
                className={`w-4 h-4 transition-transform ${
                  openFilter === key ? "rotate-180" : ""
                }`}
              />
            </button>

            {openFilter === key && (
              <div
                className="absolute z-10 mt-2 bg-white border border-gray-200 rounded-xl shadow-lg py-2 w-44 max-h-64 overflow-y-auto"
                role="listbox"
              >
                {options.map((option) => {
                  const isSelected = selectedValue === option.value;
                  return (
                    <button
                      key={option.value}
                      className={`block w-full text-left px-4 py-2 text-sm transition-all duration-150 ${
                        isSelected
                          ? "text-orange-600 font-semibold bg-orange-50"
                          : "text-gray-700 hover:bg-orange-50"
                      }`}
                      onClick={() => {
                        const newValue =
                          selectedValue === option.value ? "" : option.value;
                        onChange(key, newValue);
                        setOpenFilter(null);
                      }}
                      role="option"
                      aria-selected={isSelected}
                    >
                      {option.label}
                    </button>
                  );
                })}
              </div>
            )}
          </div>
        );
      })}
    </div>
  );
}
