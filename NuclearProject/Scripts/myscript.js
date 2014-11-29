function deletePrompt(missileId) {
    var result = confirm("Silmek istediğinize emin misiniz");
    if (result == true) {
        $.post("/Missile/Delete", { data: "ozgur", id: missileId}, function () {
                window.location = "/Missile/Index";
        });
    }
    else {
        window.location = "/Missile/Index";
    }
}