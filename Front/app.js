// app.js
(() => {
  const apiBase = "https://fichamedica.junasoft.com/Ficha";

  function showLoader() {
    document.getElementById("loader").style.display = "flex";
  }
  function hideLoader() {
    document.getElementById("loader").style.display = "none";
  }

  // --- Login view ---
  // Búsqueda de ficha (paramédico)
  const loginForm = document.getElementById("form-login");
  if (loginForm) {
    loginForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      const dniInput = document.getElementById("dni-login");
      const dni = dniInput.value.trim();
      const errorDiv = document.getElementById("dni-error");
      errorDiv.style.display = "none";

      if (!dni) {
        errorDiv.textContent = "Por favor ingrese un DNI.";
        errorDiv.style.display = "block";
        return;
      }
      showLoader();
      try {
        const res = await fetch(`${apiBase}/${dni}`);
        if (res.ok) {
          window.location.href = `ficha.html?dni=${dni}`;
        } else if (res.status === 404) {
          errorDiv.textContent =
            "DNI no encontrado. Verifique e intente de nuevo.";
          errorDiv.style.display = "block";
        } else {
          console.error("Error inesperado:", res.status);
        }
      } catch (err) {
        console.error("Error de conexión:", err);
      } finally {
        hideLoader();
      }
    });
    return;
  }

  // --- Register view ---
  const registerForm = document.getElementById("form-register");
  if (registerForm) {
    // Si venimos de login con DNI en query string, lo precargamos
    const params = new URLSearchParams(window.location.search);
    const dni = params.get("dni");
    if (dni) document.getElementById("dni-register").value = dni;

    // Mostrar/ocultar textarea de alergias
    document
      .getElementById("check-alergias")
      .addEventListener("change", (e) => {
        document.getElementById("alergias-text").style.display = e.target
          .checked
          ? "block"
          : "none";
      });

    // dentro de tu IIFE, donde detectas registerForm:
    registerForm.addEventListener("submit", async (e) => {
      e.preventDefault();

      // Construir FormData según tu FichaDTO
      const formData = new FormData();
      formData.append("Dni", document.getElementById("dni-register").value);
      formData.append("Nombre", document.getElementById("nombre").value);
      formData.append("Apellido", document.getElementById("apellido").value);
      formData.append(
        "FechaNacimiento",
        document.getElementById("fecha-nac").value
      );
      formData.append("Sexo", document.getElementById("genero").value);
      formData.append(
        "GrupoSanguineo",
        document.getElementById("grupo-sang").value || ""
      );
      formData.append(
        "EsDiabetico",
        document.getElementById("check-diabetico").checked
      );
      formData.append(
        "EsAlergico",
        document.getElementById("check-alergias").checked
      );
      formData.append(
        "Alergias",
        document.getElementById("alergias-text").value || ""
      );
      formData.append(
        "Enfermedades",
        document.getElementById("enfermedades").value || ""
      );
      formData.append(
        "MedicacionActual",
        document.getElementById("medicacion").value || ""
      );
      formData.append(
        "NombreContactoEmergencia",
        document.getElementById("contacto-nombre").value
      );
      formData.append(
        "TelefonoContactoEmergencia",
        document.getElementById("contacto-telf").value
      );
      showLoader();
      try {
        const res = await fetch(apiBase, {
          method: "POST",
          body: formData,
        });
        const json = await res.json();

        if (res.ok && json.isExitoso) {
          // Redirige a la ficha con el DNI recién creado
          window.location.href = `ficha.html?dni=${json.resultado.dni}`;
        } else {
          console.error("Error al crear ficha:", json.errorMessages);
          alert(json.errorMessages.join("\n"));
        }
      } catch (err) {
        console.error("Error de conexión al crear ficha:", err);
        alert("No se pudo conectar con el servidor.");
      } finally {
        hideLoader();
      }
    });

    return;
  }

  // --- Record view ---
  const recordContainer = document.querySelector(".med-header");
  if (recordContainer) {
    const params = new URLSearchParams(window.location.search);
    const dni = params.get("dni");
    if (!dni) {
      window.location.href = "busqueda.html";
      return;
    }

    fetch(`${apiBase}/${dni}`)
      .then((res) => res.json())
      .then((data) => {
        if (!data.isExitoso) throw new Error("Ficha no encontrada");
        const f = data.resultado;

        // Header
        const img = document.getElementById("patient-photo");
        if (f.sexo.toLowerCase() === "masculino") {
          img.src = "avatarHombre.png";
          img.alt = "Foto paciente masculino";
        } else if (f.sexo.toLowerCase() === "femenino") {
          img.src = "avatarMujer.png"; // icono mujer
          img.alt = "Foto paciente femenino";
        }
        document.getElementById(
          "patient-name"
        ).textContent = `${f.nombre} ${f.apellido}`;
        document.getElementById("patient-dni").textContent = f.dni;

        // Datos Personales
        document.getElementById("dp-dni").textContent = f.dni;
        document.getElementById("dp-nombre").textContent = f.nombre;
        document.getElementById("dp-apellido").textContent = f.apellido;
        document.getElementById("dp-fecha").textContent =
          f.fechaNacimiento?.split("T")[0] || "";
        document.getElementById("dp-genero").textContent = f.sexo;
        document.getElementById("dp-grupo").textContent =
          f.grupoSanguineo || "—";
        document.getElementById("dp-diabetico").textContent = f.esDiabetico
          ? "Sí"
          : "No";
        document.getElementById("dp-alergico").textContent = f.esAlergico
          ? f.alergias || "—"
          : "No";

        // Historial Médico
        document.getElementById("hm-enfermedades").textContent =
          f.enfermedades || "—";
        document.getElementById("hm-medicacion").textContent =
          f.medicacionActual || "—";

        // Contacto Emergencia
        document.getElementById("ce-nombre").textContent =
          f.nombreContactoEmergencia || "—";
        document.getElementById("ce-telefono").textContent =
          f.telefonoContactoEmergencia || "—";
      })
      .catch((err) => {
        console.error(err);
        window.location.href = "busqueda.html";
      });
  }
})();
