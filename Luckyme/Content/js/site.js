main = {};
main.default = 'Content/img/0.jpg';
main.items = [
    'Content/img/1.jpg',
    'Content/img/2.jpg',
    'Content/img/3.jpg',
    'Content/img/4.jpg',
    'Content/img/5.jpg',
]

main.reset = function reset(firstInit = true, groups = 1, duration = 1) {
    let doors = document.querySelectorAll('.door');
    for (const door of doors) {
        if (firstInit) {
            door.dataset.spinned = '0';
        }

        const boxes = door.querySelector('.boxes');
        const boxesClone = boxes.cloneNode(false);
        const pool = [main.default];

        if (!firstInit) {

            const arr = [];
            for (let n = 0; n < (groups > 0 ? groups : 1); n++) {
                arr.push(...main.items);
            }
            pool.push(...main.shuffle(arr));

            boxesClone.addEventListener(
                'transitionstart',
                function () {
                    door.dataset.spinned = '';
                    this.querySelectorAll('.box').forEach((box) => {
                        box.style.filter = 'blur(10px)';
                    });
                },
                { once: true }
            );

            boxesClone.addEventListener(
                'transitionend',
                function () {
                    this.querySelectorAll('.box').forEach((box, index) => {
                        box.style.filter = 'blur(0)';
                        if (index > 0) this.removeChild(box);
                    });
                  
                },
                { once: true }
            );
        }

        for (let i = pool.length - 1; i >= 0; i--) {

            //var ds = pool[i].split("/");
            //console.log(ds[3]);

            const box = document.createElement('img');
            box.classList.add('box');
            box.dataset.value = pool[i].slice(13); 
            box.src = pool[i];
            boxesClone.appendChild(box);
        }
        boxesClone.style.transitionDuration = `${duration > 0 ? duration : 1}s`;
        boxesClone.style.transform = `translateY(-${door.clientHeight * (pool.length - 1)}px)`;
        door.replaceChild(boxesClone, boxes);
    }
}

main.spin = async function Spin()
{
    main.reset(false, 1, 2);
    $(".dss").attr("disabled", true);
    let credit = $("#bet").val();

    if (credit == '') {

        alert("Bet value cannot be empty");
        $(".dss").attr("disabled", false);

    } else if (credit <= 0) {

        alert("Bet value cannot be less than or equal to zero");
        $(".dss").attr("disabled", false);

    } else {

        let balance = $("#credit").val();

        if (credit > balance) {
            alert("You do not have enough credit to complete this transcation");
            $(".dss").attr("disabled", false);
            return;
        }

        let doors = document.querySelectorAll('.door');

        async function erw() {
            let sds = [];
            for (const door of doors) {
                const boxes = door.querySelector('.boxes');
                const duration = parseInt(3);
                boxes.style.transform = 'translateY(0)';
               
                await new Promise((resolve) => setTimeout(resolve, duration * 100));

                let value = $(door).children().find("img").data("value");
                sds.push(value);    
            }

            return sds;
        }

        let values = await erw();
        $("#spin_slot1").val(values[0]);
        $("#spin_slot1").val(values[1]);
        $("#spin_slot1").val(values[3]);

        $("#spin_submitpayout").click();
    }    
}

main.shuffle = function Shuffle([...arr]) {

    let m = arr.length;
    while (m) {
        const i = Math.floor(Math.random() * m--);
        [arr[m], arr[i]] = [arr[i], arr[m]];
    }
    return arr;
}



