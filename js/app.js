// import Something from "./another-js-file.js";

class App {
    constructor() {
           
    }

    go() {
    
        this.getBooksWithoutFilter();
    }

    //parse api results into html
    renderBooksData(booksList) {

        let booksHtml = '<Table>';
        booksList.forEach(book => {
            booksHtml += `<tr><td>
                                <a href='#' onClick='javascript:getWordCount(${book.Id},"${book.BookName}");' id='${book.Id}'>${book.BookName}</a>
                                </td></tr>`;
        });

        let container = document.querySelector('.container');
        container.innerHTML = booksHtml + '</Table>';
    }

    
    //get all the books currently available
    getBooksWithoutFilter() {
        let fetchUrl = '/api/books';
        let container = document.querySelector('.container');
        container.innerHTML = '<div>Fetching Data...</div>';
        try {
            let res = fetch(fetchUrl)
                .then(response => {
                    return response.json()
                })
                .then(data => {
                  this.renderBooksData(data);
                })
        } catch (error) {
            console.log(error);
        }
    }
}
new App().getBooksWithoutFilter();

