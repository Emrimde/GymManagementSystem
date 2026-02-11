export interface ConfirmResetPasswordRequest {
  userId: string;
  token: string;
  newPassword: string;
}