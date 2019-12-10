import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Folder } from '../model/folder';
import { TokenManager } from 'src/modules/account/service/token-manager';
import { Observable } from 'rxjs';

@Injectable()
export class FolderService {
    private url = "/api/folder";
    private storageUrl = "/userstorage/folders/";

    constructor(
        private httpClient: HttpClient,
        private tokenManager: TokenManager
    ) { }

    folder(id: string): Observable<Blob> {
        return this.httpClient.get(this.url + "/stream/" + id, { headers: this.tokenManager.getHeaders(), responseType: 'blob', observe: 'body' });
    }

    post(folder: Folder): Observable<HttpResponse<string>> {
        return this.httpClient.post<string>(this.url, folder, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }

    put(folder: Folder, move : boolean = false): Observable<HttpResponse<Folder>> {
        var headers = this.tokenManager.getHeaders();
        headers = headers.append("move", move.toString());
        return this.httpClient.put<Folder>(this.url, folder, { headers: headers, observe: "response" });
    }

    get(id: string): Observable<Folder> {
        return this.httpClient.get<Folder>(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: 'body' });
    }

    getAll(): Observable<Folder[]> {
        return this.httpClient.get<Folder[]>(this.storageUrl + this.tokenManager.get().userId, { headers: this.tokenManager.getHeaders(), observe: 'body' });
    }

    delete(id: string): Observable<HttpResponse<any>> {
        return this.httpClient.delete(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }
}