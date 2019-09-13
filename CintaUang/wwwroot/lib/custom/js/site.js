function showAlert(type, message, form) {
    if (typeof (form) == "undefined") form = "";
    if (type == "Success") {
        $.bootstrapGrowl(message, {
            type: "success"
        })
    }
    else {
        $.bootstrapGrowl(message, {
            type: "danger"
        })
    }
}