import { useState, useEffect, useRef } from "react";

interface SelectMenuProps {
  value: string;
  options: { label: string; value: string }[];
  onChange: (v: string) => void;
  fieldKey: string;
  placeholder: string;
  editedFields: Record<string, boolean>;
  prefix?: string;
}

export function SelectMenu({
  value,
  options,
  onChange,
  fieldKey,
  placeholder,
  editedFields,
  prefix = "",
}: SelectMenuProps) {
  const [open, setOpen] = useState(false);
  const ref = useRef<HTMLDivElement>(null);

  const selected = options.find((o) => o.value === value);
  const hasEdited = !!editedFields[fieldKey];
  const displayText =
    hasEdited && selected ? `${prefix}${selected.label}` : placeholder;

  useEffect(() => {
    function handleClickOutside(e: MouseEvent) {
      if (ref.current && !ref.current.contains(e.target as Node)) {
        setOpen(false);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <div ref={ref} className="relative w-full">
      <button
        type="button"
        className={`flex items-center justify-between w-full px-6 py-2 rounded-full border border-gray-200 bg-white font-medium text-base focus:outline-none focus:ring-2 focus:ring-orange-400 ${
          hasEdited ? "text-black" : "text-gray-400"
        }`}
        onClick={() => setOpen((prev) => !prev)}
        aria-haspopup="listbox"
        aria-expanded={open}
        aria-labelledby={`select-${fieldKey}`}
      >
        <span className="truncate">{displayText}</span>
      </button>

      {open && (
        <ul
          className="absolute mt-2 w-full bg-white border border-gray-200 rounded-lg shadow-md z-10 max-h-60 overflow-y-auto"
          role="listbox"
          aria-labelledby={`select-${fieldKey}`}
        >
          {options.map((option) => {
            const isSelected = option.value === value;
            return (
              <li key={option.value} role="option" aria-selected={isSelected}>
                <button
                  type="button"
                  className={`w-full text-left px-4 py-2 text-sm ${
                    isSelected
                      ? "bg-orange-50 text-orange-600 font-semibold"
                      : "hover:bg-gray-100 text-gray-800"
                  }`}
                  onClick={() => {
                    onChange(option.value);
                    setOpen(false);
                  }}
                >
                  {prefix}
                  {option.label}
                </button>
              </li>
            );
          })}
        </ul>
      )}
    </div>
  );
}
