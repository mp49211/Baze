var map = function () {
    if (this.komentari !== undefined) {
        var count = 0;
        this.komentari.forEach(function (komentari) { ++count; });
        emit(this.naslov, count);

    } else {
        emit(this.naslov, 0);
    }
};
var reduce = function (key, values) {
   
    return values;

};

db.clanci.mapReduce(
    map,
    reduce,
    { out: "mr_komentari" });

db.mr_komentari.find().sort({ value: -1 });