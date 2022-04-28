export interface UserAuthModel {
    id: string;
    value: string;
    expires: Date;
};

export interface EditUserDetailsModel {
    id: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    dateOfBirth: string;
    gender: Gender;
}

export interface UserDetailsModel extends EditUserDetailsModel {
    email: string;
    roles: string[];
    emailConfirmed: boolean;
    age: number;
};

export enum Gender {
    Male = 1,
    Female = 2,
    Other = 3
}
