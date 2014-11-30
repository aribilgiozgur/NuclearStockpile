function deletePrompt(missileId) {
    var result = confirm("Silmek istediğinize emin misiniz");
    if (result == true) {
        $.post("/Missile/Delete", { data: "ozgur", id: missileId}, function (data) {
            //window.location = "/Missile/Index";
            console.log("Success");
        });
    }
    else {
        //window.location = "/Missile/Index";
    }
}