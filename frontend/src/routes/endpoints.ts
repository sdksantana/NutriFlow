const basePatient = "/patients";
const baseNutritionist = "/nutritionists";
const baseNutritionistPatient = "/nutricionists-patients"

export const Endpoints = {
  login: "/users/login",
  register: "/users/register",

 addLink: (userId: string) => `${baseNutritionistPatient}/${userId}`,
 getRecipeFilters: `${baseNutritionistPatient}/recipe-filters`,
 addRecipeLink: (nutritionistId: string) => `${baseNutritionistPatient}/recipes-link/${nutritionistId}`,
 deleteRecipeLink: (id: string, nutritionistId: string) => `${baseNutritionistPatient}/recipes-link/${id}/${nutritionistId}`,

  getPatient: (id: string) => `${basePatient}/${id}`,
  getDashboard: (patientId: string) => `${basePatient}/dashboard/${patientId}`,
  getMeasurements: (patientId: string) => `${basePatient}/body-measurements/${patientId}`,
  postMeasurements: (patientId: string) => `${basePatient}/body-measurements/${patientId}`,
  putMeasurements: (patientId: string) => `${basePatient}/body-measurements/${patientId}`,
  getMeasurementHistory: (patientId: string) => `${basePatient}/body-measurements-historic/${patientId}`,
  getIMC: (patientId: string) => `${basePatient}/tmb/${patientId}`,
  getBodyMeasurementEvolution: (patientId: string) => `${basePatient}/body-measurements-evolution/${patientId}`,
  getPatientByEmail: (email: string) => `${basePatient}/email/${email}`,

  getRecipes: (patientId: string) => `${basePatient}/recipes/${patientId}`,
  getRecipeById: (id: string, patientId: string) => `${basePatient}/recipes/${id}/${patientId}`,
  createRecipe: (patientId: string) => `${basePatient}/recipes/${patientId}`,
  updateRecipe: (id: string, patientId: string) => `${basePatient}/recipes/${id}/${patientId}`,
  deleteRecipe: (id: string, patientId: string) => `${basePatient}/recipes/${id}/${patientId}`,
  getPlanRecipes: (patientId: string) => `${basePatient}/plan-recipes/${patientId}`,
  getPlanRecipeById: (id: string, patientId: string) => `${basePatient}/plan-recipes/${id}/${patientId}`,

  getNutritionistDashboard: (nutritionistId: string) => `${baseNutritionist}/dashboard/${nutritionistId}`,
  getProfile: (nutritionistId: string) => `${baseNutritionist}/profile/${nutritionistId}`,
  createNutritionistRecipe: (nutritionistId: string) => `${baseNutritionist}/recipes/${nutritionistId}`,
  getNutritionistRecipe: (nutritionistId: string) => `${baseNutritionist}/recipes/${nutritionistId}`,
  updateNutritionistRecipe: (id: string, nutritionistId: string) => `${baseNutritionist}/recipes/${id}/${nutritionistId}`,
  getNutritionistRecipeById: (id: string, nutritionistId: string) => `${baseNutritionist}/recipes/${id}/${nutritionistId}`,
  deleteNutritionistRecipe: (id: string, nutritionistId: string) => `${baseNutritionist}/recipes/${id}/${nutritionistId}`,
};
