import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UrlService {
    private url: string = "/api/file/";

    constructor(
        private httpClient: HttpClient,
    ) { }

    getUrl(id: string) {
        return this.httpClient.get(this.url + id + "/url", { responseType: 'text' });
    }
}