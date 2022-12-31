cs = {};
cs.default = 'Images/0.jpg';
cs.items = [
    'Images/1.jpg',
    'Images/2.jpg',
    'Images/3.jpg',
    'Images/4.jpg',
    'Images/5.jpg',
]

cs.login = function Login() {

    let username = $("#username").val();
    let password = $("#password").val();
    let isvalid = true;
    let error = $("#errors");
    error.empty();

    if (username == '') {
        isvalid = false;
        error.append("<li>Username field is required!</li>")
    }

    if (password == '') {
        isvalid = false;
        error.append("<li>Password field is required!</li>")
    }

    $(".networkactivity").attr("disabled", isvalid);

    if (!isvalid) return;
      

    let obj = {};
    obj.userName = username;
    obj.password = password;

    $.ajax({
        type: "POST",
        url: 'default.aspx/Login',
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            if (response != null && response.d != null) {
                var data = response.d;
                data = $.parseJSON(data);

                if (data.succeeded) {
                    alert(data.message);
                    window.location = "/"

                } else {
                    error.append(`<li>${data.message}</li>`);
                }

            }

        }
    }).always(function () {
        $(".networkactivity").attr("disabled", false);
    });
}

cs.register = function Register()
{
    let username = $("#username").val();
    let password = $("#password").val();
    let confirmpassword = $("#confirmpassword").val();

    let error = $("#errors");
    error.empty();

    let isvalid = true;

    if (username == '') {
        isvalid = false;
        error.append("<li>Username field is required!</li>")
    }

    if (password != '') {
        if (password != confirmpassword) {
            error.append("<li>Password do not match!</li>");
            isvalid = false;
        }
    } else {
        error.append("<li>Password field is required!</li>")
        isvalid = false;
    }


    if (!isvalid) return;

    let obj = {};
    obj.userName = username;
    obj.password = password;

    $.ajax({
        type: "POST",
        url: 'default.aspx/Register',
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            if (response != null && response.d != null) {
                var data = response.d;
                data = $.parseJSON(data);

                if (data.succeeded)
                {
                    alert(data.message);
                    cs.loadPage('login', 'loadAuth')

                } else
                {
                    error.append(`<li>${data.message}</li>`);
                }

            }

        }
    }).always(function () {
        $(".networkactivity").attr("disabled", false);
    });
}


cs.loadPage = function Loadpage(pageType, containerId) {
    var obj = {};
    obj.pageType = pageType;
    $.ajax({
        type: "POST",
        url: "Default.aspx/LoadPage",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(`#${containerId}`).html(r.d);
        }
    });
}

cs.reset = function reset(firstInit = true, groups = 1, duration = 1) {
    let doors = document.querySelectorAll('.door');
    for (const door of doors) {
        if (firstInit) {
            door.dataset.spinned = '0';
        }

        const boxes = door.querySelector('.boxes');
        const boxesClone = boxes.cloneNode(false);
        const pool = [cs.default];

        if (!firstInit) {

            const arr = [];
            for (let n = 0; n < (groups > 0 ? groups : 1); n++) {
                arr.push(...cs.items);
            }
            pool.push(...cs.shuffle(arr));

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
            box.dataset.value = pool[i].slice(7); 
            box.src = pool[i];
            boxesClone.appendChild(box);
        }
        boxesClone.style.transitionDuration = `${duration > 0 ? duration : 1}s`;
        boxesClone.style.transform = `translateY(-${door.clientHeight * (pool.length - 1)}px)`;
        door.replaceChild(boxesClone, boxes);
    }
}

cs.spin = async function Spin()
{
    cs.reset(false, 1, 2);
    $(".networkactivity").attr("disabled", true);
    let credit = $("#spin_bet").val();

    if (credit == '') {

        alert("Bet value cannot be empty");
        $(".dss").attr("disabled", false);

    } else if (credit <= 0) {

        alert("Bet value cannot be less than or equal to zero");
        $(".dss").attr("disabled", false);

    } else {

        let balance = $("#spin_credits").val();

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
        var obj = {};
        obj.slot1value = values[0];
        obj.slot2value = values[1];
        obj.slot3value = values[2];
        obj.betcredit = credit;


        $.ajax({
            type: "POST",
            url: 'default.aspx/Payout',
            data: JSON.stringify(obj),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {

                if (response != null && response.d != null)
                {
                    var data = response.d;
                    data = $.parseJSON(data);

                    if (data.succeeded)
                    {
                        $("#spin_credits").val(data.credits);
                        $("#spin_wins").val(data.wins);
                        $("#extraCredit").html(data.extraCredit);

                    } else
                    {
                        cs.reset();
                        alert(data.message);
                    }
                  
                }
               
            }
        }).always(function () {
            $(".networkactivity").attr("disabled", false);
        });
 
    }    
}

cs.shuffle = function Shuffle([...arr]) {

    let m = arr.length;
    while (m) {
        const i = Math.floor(Math.random() * m--);
        [arr[m], arr[i]] = [arr[i], arr[m]];
    }
    return arr;
}

cs.addcredit = function AddCredit() {

    $(".networkactivity").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: 'default.aspx/AddCredit',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            if (response != null && response.d != null) {
                var data = response.d;
                data = $.parseJSON(data);

                if (data.succeeded)
                {
                    $("#spin_credits").val(data.credits);

                } else {
                    alert(data.message);
                    cs.logout();
                }

            }

        }
    }).always(function () {
        $(".networkactivity").attr("disabled", false);
    });
}

cs.logout = function Logout()
{
    $(".networkactivity").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: 'default.aspx/Exit',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {

            $("#loadspin").empty();
            cs.loadPage('login', 'loadAuth');
           
        }
    }).always(function () {
        $(".networkactivity").attr("disabled", false);
    });
}

