import { ValidationError } from './validation';
import { UserAuthModel } from '../user/userModels'; 

const sendGet = <Type>(url: string, auth: UserAuthModel | null = null): Promise<Type> => {
    return sendRequest<Type>('GET', url, null, auth);
}

const sendPut = <Type>(url: string, data: any) => {
    return sendRequest<Type>('PUT', url, data);
}

const sendPost = <Type>(url: string, data: any) => {
    return sendRequest<Type>('POST', url, data);
}

const sendDelete = <Type>(url: string, data: any) => {
    return sendRequest<Type>('DELETE', url, data);
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
                
                const errors = await response.json();
                if (typeof(errors) === typeof([])) {
                    reject(errors);
                    return;
                } 
                
                reject([{ key: '', errors: [ response.statusText ] }]);
            })
            .catch((response: Response)=>{
                const errors: ValidationError[] = [];
                errors.push({ key: '', errors: [ response.statusText ] })
                reject(errors);
            });
        });
    }
    
    export {
        sendGet,
        sendPost,
        sendPut,
        sendDelete
    };