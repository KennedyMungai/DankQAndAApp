import { webAPIUrl } from './AppSettings';

export interface HttpRequest<REQB> {
    path: string;
    method?:string;
    body?:REQB;
    accessToken?: string;
}

export interface HttpResponse<RESB> {
    ok: boolean;
    body?: RESB;
}

export const http = async <RESB, REQB = undefined>(config: HttpRequest<REQB>) : Promise<HttpResponse<RESB>> => {
    const request = new Request(
        `${webAPIUrl}${config.path}`,
        {
            method: config.method || 'get',
            headers: {
                'Content-Type': 'application/json'
            },
            body: config.body ? JSON.stringify(config.body) : undefined
        }
    );

    if (config.accessToken) {
        request.headers.set(
            'authorization',
            'bearer ${config.accessToken}'
        );
    }

    if (response.ok) {
        const body = await response.json();
        return { ok: Response.ok, body };
    }
    else
    {
        logError(request, response);
        return { ok: Response.ok };
    }

    console.error(
        `Error requesting ${request.method}
        ${request.url}`,
        body,
    );
};