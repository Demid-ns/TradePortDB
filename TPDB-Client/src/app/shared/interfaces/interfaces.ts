export interface User {
  email: string;
  password: string;
  returnJWTToken: boolean;
}

export interface APIResponse {
  access_token: string;
}
