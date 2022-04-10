export interface UserAuthModel {
    id: string;
    value: string;
    expires: Date;
};

export interface UserDetailsModel {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    roles: string[];
    phoneNumber: string;
    emailConfirmed: boolean;
};