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
          // Si existe, vamos a la ficha
          window.location.href = `ficha.html?dni=${dni}`;
        } else if (res.status === 404) {
          // Si no existe, redirigimos al formulario de registro, pasando el DNI
          window.location.href = `formulario.html?documento=${dni}`;
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
    const dni = params.get("documento");
    if (dni) document.getElementById("dni-register").value = dni;

    // Función genérica para toggle de textareas
    const setupDetailToggle = (radioName, detailId) => {
      document
        .querySelectorAll(`input[name="${radioName}"]`)
        .forEach((radio) => {
          radio.addEventListener("change", () => {
            const area = document.getElementById(detailId);
            if (radio.checked && radio.value === "Sí") {
              area.style.display = "block";
            } else {
              area.style.display = "none";
              area.value = "";
            }
          });
        });
    };

    setupDetailToggle("CondicionCronica", "cronicas-detalle");
    setupDetailToggle("Alergia", "alergias-detalle");
    setupDetailToggle("MedicacionRegular", "medicacion-detalle");
    setupDetailToggle("CirugiaReciente", "cirugia-detalle");
    setupDetailToggle("Droga", "droga-detalle");

    // Toggle para select "Fuma"
    const fumaSelect = document.getElementById("fuma");
    const fumaDetalle = document.getElementById("fuma-detalle");
    fumaSelect.addEventListener("change", () => {
      if (fumaSelect.value === "He dejado de fumar") {
        fumaDetalle.style.display = "block";
      } else {
        fumaDetalle.style.display = "none";
        fumaDetalle.value = "";
      }
    });

    // dentro de tu IIFE, donde detectas registerForm:
    registerForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      showLoader();

      const getRadio = (name) => {
        const sel = document.querySelector(`input[name="${name}"]:checked`);
        return sel ? sel.value : "";
      };

      const formData = new FormData();
      formData.append("Dni", document.getElementById("dni-register").value);
      formData.append("Nombre", document.getElementById("nombre").value);
      formData.append("Apellido", document.getElementById("apellido").value);
      formData.append(
        "FechaNacimiento",
        document.getElementById("fecha-nac").value
      );
      formData.append("Sexo", document.getElementById("genero").value);
      formData.append("AlturaCm", document.getElementById("altura").value);
      formData.append("PesoKg", document.getElementById("peso").value);
      formData.append("Email", document.getElementById("email").value);
      formData.append("Telefono", document.getElementById("telefono").value);
      formData.append("CondicionCronica", getRadio("CondicionCronica"));
      formData.append(
        "CondicionesCronicasDetalle",
        document.getElementById("cronicas-detalle").value
      );
      formData.append("Alergia", getRadio("Alergia"));
      formData.append(
        "AlergiasDetalle",
        document.getElementById("alergias-detalle").value
      );
      formData.append("MedicacionRegular", getRadio("MedicacionRegular"));
      formData.append(
        "MedicacionRegularDetalle",
        document.getElementById("medicacion-detalle").value
      );
      formData.append("CirugiaReciente", getRadio("CirugiaReciente"));
      formData.append(
        "CirugiaRecienteDetalle",
        document.getElementById("cirugia-detalle").value
      );
      formData.append("Fuma", document.getElementById("fuma").value);
      formData.append(
        "FumaDetalle",
        document.getElementById("fuma-detalle").value
      );
      formData.append("Droga", getRadio("Droga"));
      formData.append(
        "DrogaDetalle",
        document.getElementById("droga-detalle").value
      );
      formData.append(
        "GrupoSanguineo",
        document.getElementById("grupo-sang").value
      );
      formData.append(
        "InformacionAdicional",
        document.getElementById("info-adicional").value
      );
      formData.append(
        "AceptaTerminos",
        document.getElementById("acepto").checked
      );
      formData.append(
        "NombreContactoEmergencia",
        document.getElementById("contacto-nombre").value
      );
      formData.append(
        "TelefonoContactoEmergencia",
        document.getElementById("contacto-telf").value
      );

      try {
        const res = await fetch(apiBase, { method: "POST", body: formData });
        const json = await res.json();
        if (res.ok && json.isExitoso) {
          window.location.href = `ficha.html?dni=${json.resultado.dni}`;
        } else {
          alert(json.errorMessages?.join("\n") || "Error al crear ficha");
        }
      } catch (err) {
        console.error("Error:", err);
        alert("No se pudo conectar al servidor.");
      } finally {
        hideLoader();
      }
    });

    return;
  }

  // justo después de detectar registerForm
  const params = new URLSearchParams(window.location.search);
  const dni = params.get("documento");
  if (dni) {
    const dniInput = document.getElementById("dni-register");
    dniInput.value = dni;
    dniInput.readOnly = true; // opcional: que no lo puedan editar
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

    showLoader();
    fetch(`${apiBase}/${dni}`)
      .then((res) => res.json())
      .then((data) => {
        if (!data.isExitoso) throw new Error("Ficha no encontrada");
        const f = data.resultado;

        // Foto según sexo
        const photo = document.getElementById("patient-photo");
        if (f.sexo.toLowerCase().includes("masculino")) {
          photo.src = "avatarHombre.png";
        } else if (f.sexo.toLowerCase().includes("femenino")) {
          photo.src = "avatarMujer.png";
        } else {
          photo.src = "avatar.webp";
        }

        // Header
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
        document.getElementById("dp-altura").textContent = f.alturaCm || "";
        document.getElementById("dp-peso").textContent = f.pesoKg || "";
        document.getElementById("dp-email").textContent = f.email || "";
        document.getElementById("dp-telefono").textContent = f.telefono || "";
        document.getElementById("dp-grupo").textContent =
          f.grupoSanguineo || "";
        // Historial Médico completo
        document.getElementById("hist-cronica").textContent =
          f.condicionCronica || "No especificado";
        document.getElementById("hist-cronica-detalle").textContent =
          f.condicionCronica === "Sí"
            ? f.condicionesCronicasDetalle || "—"
            : "—";

        document.getElementById("hist-alergia").textContent =
          f.alergia || "No especificado";
        document.getElementById("hist-alergia-detalle").textContent =
          f.alergia === "Sí" ? f.alergiasDetalle || "—" : "—";

        document.getElementById("hist-medicacion-regular").textContent =
          f.medicacionRegular || "No especificado";
        document.getElementById("hist-medicacion-detalle").textContent =
          f.medicacionRegular === "Sí"
            ? f.medicacionRegularDetalle || "—"
            : "—";

        document.getElementById("hist-cirugia").textContent =
          f.cirugiaReciente || "No especificado";
        document.getElementById("hist-cirugia-detalle").textContent =
          f.cirugiaReciente === "Sí" ? f.cirugiaRecienteDetalle || "—" : "—";

        document.getElementById("hist-fuma").textContent =
          f.fuma || "No especificado";
        document.getElementById("hist-droga").textContent =
          f.droga || "No especificado";
        document.getElementById("hist-droga-detalle").textContent =
          f.droga === "Sí" ? f.drogaDetalle || "—" : "—";

        // Adicionales
        document.getElementById("dp-info-add").textContent =
          f.informacionAdicional || "";
        document.getElementById("dp-acepta").textContent = f.aceptaTerminos
          ? "Sí"
          : "No";

        // Contacto emergencia
        document.getElementById("ce-nombre").textContent =
          f.nombreContactoEmergencia || "";
        document.getElementById("ce-telefono").textContent =
          f.telefonoContactoEmergencia || "";
      })
      .catch((err) => {
        console.error(err);
        window.location.href = "busqueda.html";
      })
      .finally(hideLoader);
  }
})();

// Función genérica para toggle de detalle
function setupDetailToggle(radioName, detailTextareaId) {
  // Obtiene todos los radios de ese grupo
  const radios = document.querySelectorAll(`input[name="${radioName}"]`);
  const textarea = document.getElementById(detailTextareaId);

  radios.forEach((radio) => {
    radio.addEventListener("change", () => {
      if (radio.value === "Sí" && radio.checked) {
        textarea.style.display = "block";
      } else {
        // Si alguno distinto de "Sí" está seleccionado, ocultamos
        textarea.style.display = "none";
        textarea.value = ""; // limpia el contenido
      }
    });
  });
}
