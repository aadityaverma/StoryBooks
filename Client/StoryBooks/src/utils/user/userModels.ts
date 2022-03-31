export interface UserAuthModel {
    id: string;
    token: string;
    expires: Date;
};

export interface UserDetailsModel {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    roles: string[];
};