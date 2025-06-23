export enum Gender {
  MALE = 0,
  FEMALE = 1,
}

export enum UserType {
  NUTRITIONIST = 0,
  PATIENT = 1,
}

export interface RegisterRequest {
  email: string;
  password: string;
  userType: UserType;
  patient?: {
    firstName: string;
    lastName: string;
    gender: Gender;
  };
  nutritionist?: {
    firstName: string;
    lastName: string;
    gender: Gender;
    crn: string;
  };
}