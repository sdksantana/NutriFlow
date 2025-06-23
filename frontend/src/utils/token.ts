import { UserType } from "../types/UserTypes";

export function getUserTypeFromToken(token: string): UserType | null {
  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    return parseInt(payload["role"]) as UserType;
  } catch {
    return null;
  }
}
export function getUserIdFromToken(token: string): string | null {
  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    return payload["sub"];
  } catch {
    return null;
  }
}
