import { setValue, getValue, clearValue } from '../common/storage';
import { UserAuthModel, UserDetailsModel } from './userModels';
import { UserRoles } from '../constants';

const userDetailsKey: string = 'sb-user-details';
const userAuthKey: string = 'sb-user-credentials';

const setUserAuth = async (data: UserAuthModel | null = null) => {
    if (!!data) {
        return await setValue(userAuthKey, data);
    }
    
    return await clearValue(userAuthKey);
}

const setUserDetails = async (data: UserDetailsModel | null = null) => {
    if (!!data) {
        return await setValue(userDetailsKey, data);
    }
    
    return await clearValue(userDetailsKey);
}

const isAuthenticated = async (): Promise<boolean> => {
    const authData: UserAuthModel = await getValue(userAuthKey);
    if (!authData) {
        return false;
    }

    const expires: Date = new Date(authData.expires);
    return expires > new Date();
};

const isAdmin = async (): Promise<boolean> => {
    return isInRole(UserRoles.Admin);
};

const isAuthor = async (): Promise<boolean> => {
    return isInRole(UserRoles.Author);
};

const isInRole = async (role: string): Promise<boolean> => {
    if (!await isAuthenticated()) {
        return false;
    }

    var userData: UserDetailsModel = await getValue(userDetailsKey);
    return !!userData && userData.roles.indexOf(role) > -1;
};

const getAuthData = async (): Promise<UserAuthModel> => {
    var authData: UserAuthModel = await getValue(userAuthKey);
    return authData;
}

const getUserDetails = async (): Promise<UserDetailsModel> => {
    var userDetails: UserDetailsModel = await getValue(userDetailsKey);
    return userDetails;
}

const clearCurrentUser = async () => {
    await setUserAuth();
    await setUserDetails();
}

const useUserStore = () => {
    return {
        isAdmin,
        isAuthor,
        isAuthenticated,
        isInRole,
        setUserDetails,
        setUserAuth,
        getAuthData,
        getUserDetails,
        clearCurrentUser
    }
}

export { useUserStore };