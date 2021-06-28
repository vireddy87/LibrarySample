
var uri = '/api/books';

// function to search the entered word
function runSearch() {
    var query = $('#searchBar').val();
    if (query.length < 3) {
        $('#bookresults').empty();
        $('#error_message').text('Minimum 3 characters required to search...');
        return false;
    }
    else {
        var bookId = $('#bookId').val();
        var searchUri = uri + '/' + bookId + '?searchQuery=' + query;
       
        Search(searchUri);
        return true;
    }
}

function getWordCount(id, bookName) {
    if (id) {
        $("bookId").val(id);
        $("#bookspan").text(bookName);
    }

    var searchUri = uri + '/' + id;

    Search(searchUri)
}

function Search(searchUri) {
  
    $.getJSON(searchUri)
        .done(function (data) {
            $('#bookresults').empty();
            $('#error_message').empty();
            var table_data = '';
            $.each(data, function (key, item) {
                table_data += GetTableTRData(item);
            });
            $('#bookresults').append(table_data);
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#bookresults').empty();
            $('#error_message').text('Error Fetching Data: ' + err);
        });
}
function GetTableTRData(item) {
     return '<tr><td>' + item.Word + '</td><td> ' + item.Count + '</td></tr>';

}


