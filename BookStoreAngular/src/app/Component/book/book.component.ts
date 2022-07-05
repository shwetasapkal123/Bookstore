import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookServicesService } from 'src/app/Services/book-services.service';
import { DataserviceService } from 'src/app/Services/dataService/dataservice.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {

  booklist:any;
  booksCount:any;
  page:number = 1;
  
  filteredString = "";

  constructor(public bookService:BookServicesService,private router:Router,public dataservice:DataserviceService) { }

  ngOnInit(): void {
    this.getAllBooks();
    this.dataservice.store.subscribe(x => this.filteredString = x);
  }

  getAllBooks()
  {
    this.bookService.getallBook().subscribe((res:any)=>
    {
      console.log("res=",res.response);
      this.booklist=res.response
      this.booksCount=res.response.length
    }, error=>{
      console.log(error);
    })
  }
  quickview(book:any){
    console.log("book id", book._id);
    
    localStorage.setItem('bookId', book._id);
    this.router.navigateByUrl('dashboard/quickview/' +book._id)
}

}
