import { ValidationError } from './validation';
import { UserAuthModel } from '../user/userModels'; 

const sendGet = <Type>(url: string, auth: UserAuthModel | null = null): Promise<Type> => {
    return sendRequest<Type>('GET', url, null, auth);
}

const sendPut = <Type>(url: string, data: any, auth: UserAuthModel | null = null) => {
    return sendRequest<Type>('PUT', url, data, auth);
}

const sendPost = <Type>(url: string, data: any, auth: UserAuthModel | null = null) => {
    return sendRequest<Type>('POST', url, data, auth);
}

const sendDelete = <Type>(url: string, data: any, auth: UserAuthModel | null = null) => {
    return sendRequest<Type>('DELETE', url, data, auth);
}

const sendRequest = async <Type>(
    method: string, 
    url: string, 
    data: any | null = null, 
    auth: UserAuthModel| null = null): Promise<Type> => {
        return new Promise<Type>((resolve, reject) => {
            let headers = { 'Content-Type': 'application/json', Authorization: '' };
            const jsonData = !!data ? JSON.stringify(data) : null;
            if (!!auth) {
                headers.Authorization = 'Bearer ' + auth.value;
            }
            
            fetch(url, {
                method: method,
                headers: headers,
                body: jsonData,
            }).then(async (response: Response)=>{
                if (response.ok){
                    const data: Type = await response.json();
                    resolve(data); 
                    return;
                }
                
                if (response.status === 404) {
                    const message: string = await response.json();
                    const errors: ValidationError[] = [{ key: '', errors: [message] }];
                    reject(errors);
                    return;
                }

                if (response.status === 401) {
                    const errors: ValidationError[] = [{ key: '', errors: [ 'Not authorized!' ] }];
                    reject(errors);
                    return;
                }
                
                const errors = await response.json();
                if (typeof(errors) === typeof([])) {
                    reject(errors);
                    return;
                } 
                
                reject([{ key: '', errors: [ response.statusText ] }]);
            })
            .catch((response: Response| TypeError)=>{
                const errors: ValidationError[] = [];
                if (!response) {
                    errors.push({ key: '', errors: [ 'Something went wrong!' ] });
                    reject(errors);
                    return;
                }

                if (response as TypeError) {
                    const typeErr = response as TypeError;
                    if(typeErr.message === 'Failed to fetch'){
                        errors.push({ key: '', errors: [ 'No connection to the server!' ] });
                    } else {
                        errors.push({ key: '', errors: [ typeErr.message ] });
                    }
                    
                    reject(errors);
                } else if (response as Response){
                    const typeErr = response as Response;
                    errors.push({ key: '', errors: [ typeErr.statusText ] });
                    reject(errors);
                }
            });
        });
    }
    
    export {
        sendGet,
        sendPost,
        sendPut,
        sendDelete
    };