import { webAPIUrl } from './AppSettings';

export interface HttpRequest<REQB> {
    path: string;
}

export interface HttpResponse<RESB> {
    ok: boolean;
    body?: RESB;
}

export const http = async <RESB, REQB = undefined>(config: HttpRequest<REQB>) : Promise<HttpResponse<RESB>> => {

};