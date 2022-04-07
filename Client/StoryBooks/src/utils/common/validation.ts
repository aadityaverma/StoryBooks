export interface ValidationError{
    key: string;
    errors: string[];
}

const camelCaseRegex = /([a-z])([A-Z])/g;
const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; 

const userFriendlyName = (name: string): string => {
    return name.replace(camelCaseRegex, '$1 $2');
}

const validateIonInput = (element: HTMLIonInputElement): ValidationError => {
    const name: string  = element.getAttribute('name') || '';
    const friendlyName: string = userFriendlyName(name);
    const validation: ValidationError = { key: name, errors: []};

    if (element.required && !element.value) {
        validation.errors.push(`'${friendlyName}' is required!`);
    }

    if (!!element.value && (element.type === 'text' || element.type === 'password' || element.type === 'email' || element.type === 'tel')) {
        const val: string = element.value!.toLocaleString();
        const minLength: number = element.minlength || Number.MIN_VALUE;
        const maxLength: number = element.maxlength || Number.MAX_VALUE;

        if (val.length > maxLength) {
            validation.errors.push(`Maximum length of ${maxLength} characters is exceeded!`);
        } else if (val.length < minLength) {
            validation.errors.push(`'${friendlyName}' must be at least ${minLength} characters long!`);
        }

        if (element.type === 'email' && !emailRegex.test(val)) {
            validation.errors.push('Enter valid email!');
        }
    }

    if (!!element.value && element.type === 'number') {
        const val: number = Number(element.value);
        const min: number = Number(element.min) || Number.MIN_VALUE;
        const max: number = Number(element.max) || Number.MAX_VALUE;

        if (val > max) {
            validation.errors.push(`'${friendlyName}' is larger than the maximum accepted value of ${max}!`);
        } else if (val < min) {
            validation.errors.push(`'${friendlyName}' is smaller than the minimum accepted value of ${min}!`);
        }
    }

    return validation;
}

export{
    validateIonInput
}