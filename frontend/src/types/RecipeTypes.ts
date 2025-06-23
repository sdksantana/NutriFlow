export type Macro = {
  label: string;
  value: string;
};

export interface Recipe {
  id: string;
  name: string;
  preparationTime: Macro;
  calories: Macro;
  protein: Macro;
  fat: Macro;
  carbs: Macro;
  meal: Macro;
  description: string;
}

export type FilterOptions = {
  meal?: Macro[];
  calories?: Macro[];
  protein?: Macro[];
  fat?: Macro[];
  carbs?: Macro[];
  preparationTime?: Macro[];
};

export interface RawRecipe {
  id: string;
  name: string;
  preparationTime: string;
  calories: string;
  protein: string;
  fat: string;
  carbs: string;
  meal: string;
  description: string;
}

export interface RecipeRequest {
  name: string;
  description: string;
  preparationTime: string;
  calories: string;
  protein: string;
  fat: string;
  carbs: string;
  meal: string;
}

export interface PatientSummary {
  id: string;
  name: string;
}

export interface RecipeNutritionist {
  id: string;
  name: string;
  description: string;
  preparationTime: { label: string; value: string };
  calories: { label: string; value: string };
  protein: { label: string; value: string };
  fat: { label: string; value: string };
  carbs: { label: string; value: string };
  meal: { label: string; value: string };
  patients?: PatientSummary[];
} 
export interface RecipeLinkItem {
  patientId: string;
  patientName: string;
  recipeLinkId?: string;
}

export interface RecipeWithLinks extends Recipe {
  links?: RecipeLinkItem[];
}
