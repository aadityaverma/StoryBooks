const ApiUrl: string = 'https://localhost:7019/api/v1';
const AccountEndpoint: string = `${ApiUrl}/account`;
const LoginEndpoint: string = `${AccountEndpoint}/login`;
const ChangePasswordEndpoint: string = `${AccountEndpoint}/manage/password`;
const DateFormat = 'dd MMM yyyy';

const UserRoles = {
    User: 'User',
    Author: 'Author',
    Admin: 'Admin'
};

export { 
    ApiUrl, 
    AccountEndpoint, 
    LoginEndpoint,
    ChangePasswordEndpoint,
    UserRoles,
    DateFormat
};