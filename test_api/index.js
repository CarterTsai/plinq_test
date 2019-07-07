var express = require('express');
var app = express();
app.use(express.json());
app.use(express.urlencoded());

app.get('/', function (req, res) {
  res.send('Hello World!');
});

app.post('/', function (req, res) {
    console.log("post");
    res.json({
        "status": true,
        "status_code": 200,
        "message": req.body.id,
    });
});

app.listen(3000, function () {
  console.log('Example app listening on port 3000!');
});