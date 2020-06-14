$('#Image').change(function (e) {
    if (typeof FileReader !== "undefined") {
        var size = this.files[0].size;
        if (size > 8388608) {
            Swal.fire(
                'Error',
                'Este archivo es más grande de lo permitido, el máximo es de 8MB.',
                'warning'
            )
            $(this).val("");
        } else {
            // Creamos el objeto de la clase FileReader
            let reader = new FileReader();
            // Leemos el archivo subido y se lo pasamos a nuestro fileReader
            reader.readAsDataURL(e.target.files[0]);
            // Le decimos que cuando este listo ejecute el código interno
            reader.onload = function () {
                let preview = document.getElementById('preview'),
                    image = document.createElement('img');
                image.src = reader.result;
                preview.innerHTML = '';
                preview.append(image);
            };
        }
    }
});



