import { useEffect, useRef, useState } from "react";

export function useFilterControls() {
  const [selectedFilters, setSelectedFilters] = useState<Record<string, string>>({});
  const [openFilter, setOpenFilter] = useState<string | null>(null);
  const filterRef = useRef<HTMLDivElement | null>(null);

  function handleFilterChange(key: string, value: string) {
    setSelectedFilters((prev) => {
      const updated = { ...prev };
      if (!value) {
        delete updated[key];
      } else {
        updated[key] = value;
      }
      return updated;
    });
  }

  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (
        filterRef.current &&
        !filterRef.current.contains(event.target as Node)
      ) {
        setOpenFilter(null);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return {
    selectedFilters,
    handleFilterChange,
    openFilter,
    setOpenFilter,
    filterRef,
  };
}
