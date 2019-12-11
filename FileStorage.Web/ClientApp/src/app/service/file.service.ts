import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { FileModel } from '../model/file';
import { TokenManager } from 'src/app/modules/account/service/token-manager';
import { Observable } from 'rxjs';

@Injectable()
export class FileService {
    private url = "/api/file";
    private getAllUrl = "/userstorage/files";

    constructor(
        private httpClient: HttpClient,
        private tokenManager: TokenManager,
    ) { }

    download(id: string): Observable<Blob> {
        return this.httpClient.get(this.url + "/stream/" + id, { headers: this.tokenManager.getHeaders(), responseType: 'blob', observe: 'body' });
    }

    downloadMany(ids: string[]): Observable<Blob> {
        return this.httpClient.post(this.url + "/stream", ids, { headers: this.tokenManager.getHeaders(), responseType: 'blob', observe: 'body' });
    }

    post(file: FileModel): Observable<HttpResponse<string>> {
        const formData = new FormData();
        formData.append('id', file.id);
        formData.append('path', file.path);
        formData.append('file', file.file);
        formData.append('userId', file.userId);
        formData.append('fileName', file.fileName);
        formData.append('folderId', file.folderId);
        formData.append('shortLink', file.shortLink);
        formData.append('categoryId', file.categoryId);
        formData.append('fileAccessibility', file.fileAccessibility.toString());
        return this.httpClient.post<string>(this.url, formData, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }

    put(file: FileModel, move: boolean = false): Observable<HttpResponse<FileModel>> {
        file.file = null;
        var headers = this.tokenManager.getHeaders();
        headers = headers.append("move", move.toString());
        return this.httpClient.put<FileModel>(this.url, file, { headers: headers, observe: 'response' });
    }

    get(id: string): Observable<FileModel> {
        return this.httpClient.get<FileModel>(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: 'body' });
    }

    getAll(): Observable<FileModel[]> {
        return this.httpClient.get<FileModel[]>(this.getAllUrl + "/" + this.tokenManager.get().userId, { headers: this.tokenManager.getHeaders(), observe: 'body' });
    }

    delete(id: string): Observable<HttpResponse<any>> {
        return this.httpClient.delete(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }

    deleteMany(ids: string[]): Observable<HttpResponse<any>> {
        return this.httpClient.post(this.url + "/delete", ids, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }
}