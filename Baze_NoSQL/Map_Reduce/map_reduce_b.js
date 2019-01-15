var map = function () {
    if (this.tekst) {
        var rijeci = this.tekst.toLowerCase().replace(/[.,\/#!$%\^&\*;:{}=\-_`~()]/g, "").replace(/\n/g, " ").split(" ");
        for (var i = 0; i < rijeci.length; i++) {
            if (rijeci[i])
                var value = { rijec: rijeci[i], broj: 1 };
                emit(this.autor, value);
        }
    }

};

var reduce = function (key, values) {
    var rv = {
        rijeci: []
    };

    var pojavljivanje = [];
    var brojanje = [];

    values.forEach(function (value) {
        if (pojavljivanje.indexOf(value.rijec) > -1) {
            for (var i = 0; i < brojanje.length; i++) {
                if (brojanje[i].rijec === value.rijec)
                    brojanje[i].broj += 1;
            }
        } else {
            pojavljivanje.push(value.rijec);
            brojanje.push({ rijec: value.rijec, broj: value.broj });
        }
    });
    var sortiranje = brojanje.sort(function (a, b) {
        return b.broj - a.broj;
    });

    for (var i = 0; i < 10; i++)
        rv.rijeci[i] = sortiranje[i];

    return rv;

};

db.clanci.mapReduce(
    map,
    reduce,
    { out: "mr_rijeci" });

db.mr_rijeci.find().pretty();