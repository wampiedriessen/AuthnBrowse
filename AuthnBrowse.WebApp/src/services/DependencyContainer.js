export default class DependencyContainer {
    containers = {};

    registerContainer(key, service) {
        this.containers[key] = service;
    }

    resolve(key) {
        if(!this.containers.hasOwnProperty(key))
            throw Error(`'${key}': No such service found in container`);
        
        return this.containers[key];
    }
}