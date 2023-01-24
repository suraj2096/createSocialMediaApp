import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment } from './comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private httpclient:HttpClient) { }

  getComment(id:number)
  {
    return this.httpclient.get<any>("https://localhost:44304/api/comment/"+id);
  }
  createComment(comment:Comment):Observable<any>
  {
    return this.httpclient.post<any>("https://localhost:44304/api/comment",comment);
  }
}
