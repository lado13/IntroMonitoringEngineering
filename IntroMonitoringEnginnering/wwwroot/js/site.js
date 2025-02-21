
function toggleImageSize(img) {
    const allImages = document.querySelectorAll("img");

    allImages.forEach((image) => {
        if (image !== img) {
            image.style.width = "100px";
            image.style.height = "100px";
           
        }
    });

    if (img.style.width === "100px" || img.style.width === "") {
        img.style.width = "400px";
        img.style.height = "400px";
        img.style.borderRadius = "0%"; 
    } else {
        img.style.width = "100px";
        img.style.height = "100px";
        img.style.borderRadius = "50%";
    }
}


function resetAllImages() {
    const allImages = document.querySelectorAll(".content-img");
    allImages.forEach((image) => {
        image.style.width = "100px";
        image.style.height = "100px";
        image.style.borderRadius = "50%";


    });
}


document.body.addEventListener("mouseout", (event) => {

    if (event.target.tagName !== "IMG") {
        resetAllImages();
    }
});



const starContainer = document.querySelector('.stars');


for (let i = 0; i < 100; i++) {
    const star = document.createElement('div');
    star.className = 'star';
    star.style.top = Math.random() * 100 + '%';
    star.style.left = Math.random() * 100 + '%';
    star.style.animationDelay = Math.random() * 2 + 's';
    starContainer.appendChild(star);
}


