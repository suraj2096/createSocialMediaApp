import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from './post';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private httpclient:HttpClient) { }
  getAllPost():Observable<any>{
    //send jwt to the user 
    // var currentuser =null;
    // var header = new HttpHeaders();
    // var currentUserSession = sessionStorage.getItem("currentUser");
    // console.log(currentUserSession);
    // if(currentUserSession !=null){
    //   //console.log("hello");
    //   currentuser = JSON.parse(currentUserSession);
    //   console.log(currentuser.user.token);

    //   header = header.set('Authorization',"Bearer "+currentuser.user.token);
    // }
    return this.httpclient.get<any>("https://localhost:44304/api/post");
  }
  
  createPost(post:Post,file:any):Observable<any>{
    // console.log(this.formdata);
    // console.log(typeof(this.formdata));
    //alert(this.newPost.title);
    console.log(post);
    const formdata = new FormData();
    var jsondata = JSON.stringify(post);
    if(file){
      //this.formdata.append('file',file,file.name);
      formdata.append("blob",file);
    }
    //var blobdata = new Blob([JSON.stringify(file)],{type:"form"});

    formdata.append("data",jsondata);
  return  this.httpclient.post<any>("https://localhost:44304/api/post",formdata);
  }
  deletePostlist(id:number):Observable<any>{
   return this.httpclient.get<any>("https://localhost:44304/api/post?id="+id);
  }
  deletePost(id:number):Observable<any>{
    return this.httpclient.delete<any>("https://localhost:44304/api/post/"+id);
  }
  likePost(post:any):Observable<any>{
    const formdata = new FormData();
    var jsondata = JSON.stringify(post);
    formdata.append("data",jsondata);
    return this.httpclient.post<Post>("https://localhost:44304/api/post/LikePost",formdata);
  }
  followuser(post:any):Observable<any>{
    const formdata = new FormData();
    var jsondata = JSON.stringify(post);
    formdata.append("data",jsondata);
    return this.httpclient.post<Post>("https://localhost:44304/api/post/FollowPerson",formdata);
  }
}
