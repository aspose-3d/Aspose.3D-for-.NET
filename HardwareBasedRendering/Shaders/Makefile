
GLSL = glslangValidator.exe

GLSL_FLAGS = -Iinc
ODIR = bin

SRC = $(wildcard *.frag *.vert)

OBJ = $(patsubst %.frag,$(ODIR)/%Fragment.spv,$(patsubst %.vert,$(ODIR)/%Vertex.spv,$(SRC)))

.PHONY: all clean

all: $(OBJ)

bin/%Fragment.spv: %.frag
	@$(GLSL) $(GLSL_FLAGS) -V $< -o $@

bin/%Vertex.spv: %.vert
	@$(GLSL) $(GLSL_FLAGS) -V $< -o $@

clean:
	rm -f bin/*.spv
