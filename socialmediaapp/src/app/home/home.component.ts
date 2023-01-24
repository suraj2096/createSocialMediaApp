import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Comment } from '../comment';
import { CommentService } from '../comment.service';
import { Post } from '../post';
import { PostService } from '../post.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  ngOnInit(): void {
    this.getall();
  }
constructor(private postservice:PostService,private router:Router,private commentservice:CommentService,private datepipe:DatePipe){};
postlist:Post[] = [];
newPost:Post = new Post();
commentlist:Comment[] = [];
newcomment:Comment= new Comment();
getspecifiecpost:Post[]=[];
date:Date=new Date();
userid:any=sessionStorage.getItem("currentUser");
parseid:any = JSON.parse(this.userid).user.id;
getall(){
  //this.newPost.publish = Date.now();
  this.postservice.getAllPost().subscribe(
    (response)=>{
      response.filter((ele:any)=>{
        this.date = new Date(ele.publish);
        ele.publish = this.date.toDateString();
      
      })
        this.postlist = response;
        //console.log(response);
    },
    (error)=>{
    }
  )
}
sendPost(){
 this.newPost.publish = this.datepipe.transform(this.date,'MM/dd/yyyy');
 //this.newPost.publish = this.newPost.publish ? this.newPost.publish.toLocaleString() : null;
 console.log(this.newPost.publish);
  console.log(Date.now());
  this.postservice.createPost(this.newPost,this.file).subscribe((response)=>{
    console.log(response);
    this.getall();
  }
  ,
  (error)=>{
      console.log(error);
  })
}
createimagepath(image:any){
  return `https://localhost:44304\\wwwroot`+image;
}
file:any;
uploadfile(event:any){
  this.file = event.srcElement.files[0];
  console.log(this.file);
}
getComment(id:number){
  console.log(id);
  this.newcomment.postid=id;
  this.commentservice.getComment(id).subscribe(
    (response)=>{
      this.commentlist = response;
      console.log(response);
    },
    (error)=>{
      // if(error.status==401){
      //   this.router.navigateByUrl("login");
      // }
      console.log(error);
    }
    )
    
}
sendcomment(){
  if(this.newcomment.message == ""){
    return;
  }
  //this.newcomment.postid = ;
  this.commentservice.createComment(this.newcomment).subscribe((response)=>{
   console.log(response);
  },
  (error)=>{
    console.log(error);
  })
  this.newcomment.message="";
  this.getComment(this.newcomment.id);
 
}
// get the remove post list
removePostList(){
  this.postservice.deletePostlist(1).subscribe((response)=>{
   this.getspecifiecpost = response;
   console.log(response);
  },
  (error)=>{
      console.log(error);
  }
  )
}
removepost(id:number){
this.postservice.deletePost(id).subscribe((response)=>{
 this.getall();
},(error)=>{

})
}

// like and unlike post related code
LikePost(post:Post){
  post.like=true;
this.postservice.likePost(post).subscribe((data)=>{
 this.getall();
},
(err)=>{
console.log(err);
})
}
UnLikePost(post:Post){
  post.like = false;
  this.postservice.likePost(post).subscribe((data)=>{
    this.getall();
   },
   (err)=>{
   console.log(err);
   })
}
// follow and unfollow realted code
follow(post:Post){
  post.follow=true;
  this.postservice.followuser(post).subscribe((data)=>{
   this.getall();
  },
  (err)=>{
  console.log(err);
  })
}
unFollow(post:Post){
  post.follow=false;
  this.postservice.followuser(post).subscribe((data)=>{
   this.getall();
  },
  (err)=>{
  console.log(err);
  })
}
}

