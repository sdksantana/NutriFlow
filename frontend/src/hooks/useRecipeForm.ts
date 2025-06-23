import { useState } from "react";

export function useRecipeForm<T extends object>(initial: T) {
  const [form, setForm] = useState<T>(initial);
  const [editedFields, setEditedFields] = useState<Record<string, boolean>>({});

  function updateField<K extends keyof T>(key: K, value: T[K]) {
    setForm((prev) => ({ ...prev, [key]: value }));
    setEditedFields((prev) => ({ ...prev, [key]: true }));
  }

  const changedValues: Partial<T> = Object.entries(editedFields).reduce(
    (acc, [key, wasEdited]) => {
      if (wasEdited) {
        acc[key as keyof T] = form[key as keyof T];
      }
      return acc;
    },
    {} as Partial<T>
  );

  return {
    form,
    setForm,
    editedFields,
    updateField,
    changedValues,
  };
}
