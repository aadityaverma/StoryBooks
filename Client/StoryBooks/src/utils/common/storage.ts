import { Storage } from '@ionic/storage';

let _store: Storage;

const getStorage = async (): Promise<Storage> => {
    if (_store == null) {
        _store = await new Storage().create();
    }

    return _store;
}

const setValue = async (key: string, value: any): Promise<any> => {
    const store = await getStorage();
    return await store.set(key, value);
};

const getValue = async (key: string): Promise<any> => {
    const store = await getStorage();
    return await store.get(key);
};

export {
    getValue,
    setValue
};