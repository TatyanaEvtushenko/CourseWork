import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AppConfig {
    private config: any = null;
    private env: any = null;

    constructor(private http: Http) { }

    public getConfig(key: any) {
        return this.config[key];
    }

    public getEnv(key: any) {
        return this.env[key];
    }

    public load() {
        return new Promise((resolve, reject) => {
            this.http.get('env.json').map(res => res.json()).catch((error: any): any => {
                this.reportError('Configuration file "env.json" could not be read', resolve);
                return Observable.throw(error.json().error || 'Server error');
            }).subscribe((envResponse) => {
                this.env = envResponse;
                let request = this.getRequest(resolve);
                this.processRequest(request, resolve);
            });
        });
    }

    private reportError(error: string, resolve: any) {
        console.error(error);
        resolve(true);
    }

    private getRequest(resolve: any) {
        let request: any = null;
        switch (this.env.env) {
        case 'production': case 'development': {
            request = this.http.get('config.' + this.env.env + '.json');
        } break;
        case 'default': {
            this.reportError('Environment file is not set or invalid', resolve);
        } break;
        }
        return request;
    }

    private processRequest(request: Observable<any>, resolve: any) {
        if (request) {
            request.map(res => res.json()).catch((error: any) => {
                    this.reportError('Error reading ' + this.env.env + ' configuration file', resolve);
                    return Observable.throw(error.json().error || 'Server error');
                })
                .subscribe((responseData) => {
                    this.config = responseData;
                    resolve(true);
                });
        } else {
            this.reportError('Env config file "env.json" is not valid', resolve);
        }
    }
}