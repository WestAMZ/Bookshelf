export interface UserCredentials {
    email: string;
    password: string;
}

export interface AuthenticationResponse {
    email: string;
    token: string;
    expiration: string;
}
