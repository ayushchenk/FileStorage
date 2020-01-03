import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Category } from '../model/category';
import { TokenManager } from 'src/app/modules/account/service/token-manager';
import { Observable } from 'rxjs';

@Injectable()
export class CategoryService {
    private url = "/api/category";

    constructor(
        private httpClient: HttpClient,
        private tokenManager: TokenManager
    ) { }

    post(category: Category): Observable<HttpResponse<any>> {
        return this.httpClient.post(this.url, category, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }

    put(category: Category): Observable<HttpResponse<any>> {
        return this.httpClient.put(this.url, category, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }

    get(id: string): Observable<Category> {
        return this.httpClient.get<Category>(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: 'body', });
    }

    getAll(): Observable<Category[]> {
        return this.httpClient.get<Category[]>(this.url, { observe: 'body' });
    }

    delete(id: string): Observable<HttpResponse<any>> {
        return this.httpClient.delete(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }
}