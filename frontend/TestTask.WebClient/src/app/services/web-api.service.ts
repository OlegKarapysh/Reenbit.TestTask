import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class WebApiService {
    private readonly apiUrl = environment.apiUrl;

    constructor(private httpClient: HttpClient) {}

    public uploadForm(file: File, email: string): Observable<Object> {
        const formData = new FormData();
        formData.append('file', file);
        formData.append('email', email);

        return this.httpClient.post(`${this.apiUrl}/api/uploadfile`, formData);
    }
}
