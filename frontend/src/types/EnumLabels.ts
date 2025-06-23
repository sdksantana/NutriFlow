import { Gender, UserType } from './UserTypes';

export const GenderLabels: Record<Gender, string> = {
  [Gender.MALE]: 'Masculino',
  [Gender.FEMALE]: 'Feminino',
};

export const UserTypeLabels: Record<UserType, string> = {
  [UserType.NUTRITIONIST]: 'Nutricionista',
  [UserType.PATIENT]: 'Paciente',
};
