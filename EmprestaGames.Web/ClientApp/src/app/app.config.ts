import { environment } from '../environments/environment';
import { InjectionToken } from '@angular/core';

export interface IAppConfig {
    endpoint: string;
}

export const AppConfig: IAppConfig = environment;

export const APP_CONFIG = new InjectionToken<IAppConfig>('app.config');
