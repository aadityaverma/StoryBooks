import { setValue, getValue } from '../common/storage';
import { UserAuthModel, UserDetailsModel } from './userModels';
import { UserRoles } from '../constants';

const userDetailsKey: string = 'sb-user-details';
const userAuthKey: string = 'sb-user-credentials';

const setUserAuth = async (data: UserAuthModel) => {
    return await setValue(userAuthKey, data);
}

const setUserDetails = async (data: UserDetailsModel) => {
    return await setValue(userDetailsKey, data);
}

const isAuthenticated = async (): Promise<boolean> => {
    var authData: UserAuthModel = await getValue(userAuthKey);
    return !!authData && authData.expires > new Date();
};

const isAdmin = async () => {
    return isInRole(UserRoles.Admin);
};

const isAuthor = async () => {
    return isInRole(UserRoles.Author);
};

const isInRole = async (role: string) => {
    if (!await isAuthenticated()) {
        return false;
    }

    var userData: UserDetailsModel = await getValue(userDetailsKey);
    return !!userData && userData.roles.indexOf(role) > -1;
};

const getAuthToken = async (): Promise<string> => {
    if (!await isAuthenticated()) {
        return '';
    }

    var authData: UserAuthModel = await getValue(userAuthKey);
    return authData?.token;
}

const getUserDetails = async (): Promise<UserDetailsModel> => {
    var userDetails: UserDetailsModel = await getValue(userDetailsKey);
    return userDetails;
}

export {
    isAdmin,
    isAuthor,
    isAuthenticated,
    isInRole,
    setUserDetails,
    setUserAuth,
    getAuthToken,
    getUserDetails
};