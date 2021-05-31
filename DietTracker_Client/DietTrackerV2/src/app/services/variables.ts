import { InjectionToken } from '@angular/core';
import { environment } from 'src/environments/environment';

export const BASE_PATH = new InjectionToken<string>(environment.apiBase);
export const COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
}
