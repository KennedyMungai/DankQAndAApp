import { webAPIUrl } from './AppSettings';

export interface HttpRequest<REQB> {
    path: string;
}

export interface HttpResponse<RESB> {
    ok: boolean;
    body?: RESB;
}

export const http = async <RESB, REQB = undefined>(config: HttpRequest<REQB>) : Promise<HttpResponse<RESB>> => {
    const request = new Request(
        `${webAPIUrl}${config.path}`
    );

    if (response.ok) {
        const body = await response.json();
        return { ok: Response.ok, body };
    }
    else
    {
        logError(request, response);
        return { ok: Response.ok };
    }
};